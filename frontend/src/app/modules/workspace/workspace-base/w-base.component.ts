import { Component, OnInit } from '@angular/core';
import { UserDTO } from 'src/app/models/user/user-dto';
import { RegistrationService } from 'src/app/services/registration.service';
import { map, Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-w-base',
  templateUrl: './w-base.component.html',
  styleUrls: ['./w-base.component.css']
})
export class WorkspaceBaseComponent implements OnInit {

  currentUser: UserDTO = {} as UserDTO;
  isExpanded: boolean = false;

  private unsubscribe$ = new Subject<void>();

  constructor(
    private registrationService: RegistrationService
  ) {}

  ngOnInit(): void {
      this.getAutorithedUser()
  }

  private getAutorithedUser() {
    return this.registrationService
      .getUser()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((user) => {
        this.currentUser = user;
      });
  }

  public openSidenav() {
    this.isExpanded = true;
  }

  public closeSidenav() {
    this.isExpanded = false;
  }
}
