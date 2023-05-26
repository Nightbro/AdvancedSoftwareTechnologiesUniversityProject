export class PharmacyMedicine {
  IdMedicine: number;
  IdPharmacy: number;
  Price: number;
  Quantity: number;
}
export class Pharmacy {
  Id: number;
  Name: string;
  OwnerId: number;
  Latitude: number;
  Longitude: number;
  Description: string;
  Medicines: PharmacyMedicine[];
}
