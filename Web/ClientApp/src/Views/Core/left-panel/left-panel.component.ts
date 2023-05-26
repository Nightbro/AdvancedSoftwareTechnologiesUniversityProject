import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../../Core/Service/authentication.service';
import { List } from 'linqts';
import { Claim } from '../../../Core/Models/claim';
import { AfterViewInit } from '@angular/core';

@Component({
  selector: 'app-left-panel',
  templateUrl: './left-panel.component.html',
  styleUrls: ['./left-panel.component.scss']
})
export class LeftPanelComponent implements OnInit, AfterViewInit {
  claims: List<Claim> = null;

  public viewInitialized: boolean = false;
  public loggedFlag: boolean = false;


  constructor(private auth: AuthenticationService) { }


  containsClaim(requestedClaimId: number) {
    if (!this.claims) {
      if (this.auth.user) {
        let role = this.auth.user.Role; 
        this.claims = new List<Claim>(role.Claims); 
      } 
      
    } 
    if (this.claims) return this.claims.Any(x => x.Id == requestedClaimId);  
    return false;
  }
  ngOnInit(): void {
  }
  ngAfterViewInit(): void {
    this.auth.IsAuthenticated().subscribe(resp => {
      this.loggedFlag = resp;
      this.viewInitialized = true;
    });
  }

}
