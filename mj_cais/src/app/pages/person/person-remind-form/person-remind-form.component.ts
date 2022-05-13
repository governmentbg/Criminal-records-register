import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, ActivatedRouteSnapshot } from "@angular/router";

@Component({
  selector: "cais-person-remind-form",
  templateUrl: "./person-remind-form.component.html",
  styleUrls: ["./person-remind-form.component.scss"],
})
export class PersonRemindFormComponent implements OnInit {
  public personToConnectId: string;
  constructor(private route: ActivatedRoute) {}
  ngOnInit(): void {
    this.personToConnectId = this.route.snapshot.params["ID"];
  }
}
