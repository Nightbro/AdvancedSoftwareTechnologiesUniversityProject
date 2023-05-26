import { HttpClient } from '@angular/common/http';
import { AfterViewInit, Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { map, startWith } from 'rxjs/operators';
import { Medicine } from '../../Core/Models/medicine';
import { Pharmacy, PharmacyMedicine } from '../../Core/Models/pharmacy';
import { PharmacyService } from '../../Core/Service/pharmacy.service';
import { ConfigService } from '../../Helpers/Config/config.service';
import { List } from 'linqts';


interface medicineInMarker {
  name: string;
  quantity: number;
  singlePrice: number;
  totalPrice: number;
}
interface marker {
  lat: number;
  lng: number;
  label?: string;
  draggable: boolean;
  description: string;
  name?: string;
  icon?: string;
  distance?: string;
  distanceInNumber?: number;
  duration?: string;
  durationInNumber?: number;
  pharmacy?: Pharmacy;

  containsAll?: boolean;
  containsOne?: boolean;
  cheapest?: boolean;
  closest?: boolean;
  allAvailableMedicine?: medicineInMarker[];
  totalPrice?: number;
  
}

class filteredMedicine {
  medicine?: Medicine;
  quantity?: number;
  get name(): string {
    if (this.medicine) return this.medicine.Name;
    return "Couldnt get medicine's name";
  }
}

class medicineFilter {
  medicine: Medicine;
  quantity: number = 0;
}

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit, AfterViewInit {



  constructor(private service: PharmacyService, private _snackBar: MatSnackBar, private http: HttpClient, private config: ConfigService) {
    //http.get<WeatherForecast[]>(baseUrl + 'api/SampleData/WeatherForecasts').subscribe(result => {
    //  this.forecasts = result;
    //}, error => console.error(error));
  }

  ngOnInit(): void {
    if (!navigator.geolocation) {
      console.log('location is not supported');
    }
    navigator.geolocation.getCurrentPosition((position) => {
      const coords = position.coords;
      const latLong = [coords.latitude, coords.longitude];
      console.log(
        `lat: ${position.coords.latitude}, lon: ${position.coords.longitude}`
      );

      this.yourLocation.lat = coords.latitude;
      this.yourLocation.lng = coords.longitude;
      this.calculateDistance();

    });


    

  }

  filterOption: string = "1";
  quantityfilter: string = "1";
  selectedMedicine: medicineFilter[] = [];
  allMedicine: medicineFilter[] = [];
  ApplyFilters() {
    switch (this.filterOption) {
      case "1": this.CheapestFilter(); break;
      case "2": this.ClosestFilter(); break;
      case "3": this.FilterWithAllMedicines(); break;
      case "4": this.FilterWithAtLeastOneMedicine(); break;

    }
  }


  updateMedicine(med: medicineFilter) {
    med.quantity = +this.quantityfilter;
  }
  loadingMarkers: boolean = false;


  checkIfContainsMedicineAndReturnIt(ph: Pharmacy, barcode: number): PharmacyMedicine {
    let all = new List<PharmacyMedicine>(ph.Medicines);
    return all.Where(x => x.IdMedicine == barcode).FirstOrDefault();
  }
  applyFilterOnMarkers() {
    this.loadingMarkers = true;
    let closestMarker: marker = null;
    let cheapestMarker: marker = null;
    for (let marker of this.markers) {
      marker.containsAll = false;
      marker.cheapest = false;
      marker.icon = null;
      marker.closest = false;
      marker.allAvailableMedicine = [];
      marker.totalPrice = 0;
      marker.containsOne = false;

      let totalAvailable = 0;
      for (let request of this.selectedMedicine) {
        let medicine = this.checkIfContainsMedicineAndReturnIt(marker.pharmacy,request.medicine.Id);
        if (medicine) {
          if (request.quantity <= medicine.Quantity) {
            marker.containsOne = true;
            marker.totalPrice += request.quantity * medicine.Price;
            marker.allAvailableMedicine.push({
              name: request.medicine.Name,
              quantity: request.quantity,
              singlePrice: medicine.Price,
              totalPrice: request.quantity * medicine.Price
            });
            totalAvailable++;
          }
        }
      }
      if ((totalAvailable == this.selectedMedicine.length) && (totalAvailable !=0)) {
        marker.containsAll = true;
      }

    }
    for (let marker of this.markers) {
      if (marker.containsAll) {
        if (!closestMarker) closestMarker = marker;
        if (!cheapestMarker) cheapestMarker = marker;

        if (marker.distanceInNumber < closestMarker.distanceInNumber) closestMarker = marker;
        if (marker.totalPrice < closestMarker.totalPrice) cheapestMarker = marker;
      }
    }


    if (closestMarker) closestMarker.closest = true;
    if (cheapestMarker) cheapestMarker.cheapest = true;


    this.loadingMarkers = false;
  }

  CheapestFilter() {
    this.applyFilterOnMarkers();

    for (let marker of this.markers) {
      if (marker.containsAll) {
        if (marker.cheapest) {
          marker.icon = "http://maps.google.com/mapfiles/ms/icons/green-dot.png";
        } else {
          marker.icon = "http://maps.google.com/mapfiles/ms/icons/yellow-dot.png";
        }
      }
    }
  }

  ClosestFilter() {
    this.applyFilterOnMarkers();

    for (let marker of this.markers) {
      if (marker.containsAll) {
        if (marker.closest) { 
          marker.icon = "http://maps.google.com/mapfiles/ms/icons/green-dot.png";
        } else {
          marker.icon = "http://maps.google.com/mapfiles/ms/icons/yellow-dot.png";
        }
      }
    }

  }

  FilterWithAllMedicines() {
    this.applyFilterOnMarkers();

    for (let marker of this.markers) {
      if (marker.containsAll) marker.icon = "http://maps.google.com/mapfiles/ms/icons/green-dot.png";
    }
  }

  FilterWithAtLeastOneMedicine() {
    this.applyFilterOnMarkers();

    for (let marker of this.markers) {
      if (marker.containsAll) {
        if (marker.containsAll) marker.icon = "http://maps.google.com/mapfiles/ms/icons/green-dot.png";
      } else  if (marker.containsOne) marker.icon ="http://maps.google.com/mapfiles/ms/icons/yellow-dot.png";
    }

  }
  private pathAPI = this.config.setting['PathAPI'];

  calculatedProperly: boolean = true;
  calculateDistance(): void {
    let url = "";
    let yourLoc = this.yourLocation.lat + "," + this.yourLocation.lng;

    let destParam = "";
    for (let x of this.markers) {
      destParam += "|" + x.lat + "," + x.lng;
    }
    destParam = destParam.replace("|", "");
    url += destParam;
    this.calculatedProperly = true;
    this.http.get<any>(this.pathAPI + 'PharmacyPublic?stringUrl=' + encodeURI(yourLoc) + '&destination=' + encodeURI(destParam)).subscribe(resp => {
      console.log(resp.rows[0]);

      if (resp&& resp.rows && resp.rows[0] && resp.rows[0].elements) {
        for (let x in resp.rows[0].elements) {
          this.markers[+x].distance = resp.rows[0].elements[+x].distance.text;
          this.markers[+x].distanceInNumber = resp.rows[0].elements[+x].distance.value;
          this.markers[+x].duration = resp.rows[0].elements[+x].duration.text;
          this.markers[+x].durationInNumber = resp.rows[0].elements[+x].duration.value;

        }
      }

    }, error => {
      if (destParam !== "") { 
      this.calculatedProperly = false;

      this._snackBar.open("There was an issue with calculating distances between location; Please try again later.", "Ok");
      }
    });
  }
  ngAfterViewInit(): void {
    this.service.GetPharmacies().subscribe(resp => {
      for (let r of resp) {
        this.markers.push({
          lat: r.Latitude,
          lng: r.Longitude,
          description: r.Description,
          name: r.Name,
          pharmacy: r,
          draggable: false
        });
      }
      this.calculateDistance();

    }, error => {
      this._snackBar.open("Could not retrieve pharmacies; Please refresh the page. Reason:" + error, "Ok");
    });
    this.allMedicine = [];
    this.service.GetMedicine().subscribe(resp => {
      for (let r of resp) {
        
        let newOpt = new medicineFilter();
        newOpt.medicine = r;


        this.allMedicine.push(newOpt)

      }
    }, error => {
      this._snackBar.open("Could not retrieve medicines; Please refresh the page. Reason:" + error, "Ok");
    }); 
    this.calculateDistance(); 
  } 

  zoom: number = 12;

  // initial center position for the map
  lat: number = 44.8085;// 51.673858;
  lng: number = 20.5716;//7.815982;

  clickedMarker(label: string, index: number) {
    console.log(`clicked the marker: ${label || index}`)
  }

  mapClicked($event) {

  }

  markerDragEnd(m: marker, $event) {
    m.lat = $event.coords.lat;
    m.lng = $event.coords.lng;
    this.calculateDistance();
  }

  yourLocation: marker = {
    lat: 44.8085,
    lng: 20.5716,
    label: "",
    draggable: true,
    description: "<strong>Your current location</strong><br /> <i> Move to adjust</i>",

    icon: "http://maps.google.com/mapfiles/ms/icons/blue-dot.png"
  }
  markers: marker[] = [ ]
}

