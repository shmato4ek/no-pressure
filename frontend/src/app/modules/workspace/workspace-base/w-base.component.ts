import { Component, OnInit } from '@angular/core';
import { UserDTO } from 'src/app/models/user/user-dto';
import { RegistrationService } from 'src/app/services/registration.service';
import { map, Subject, takeUntil } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { CacheResourceService } from 'src/app/services/cache.resource.service';

@Component({
  selector: 'app-w-base',
  templateUrl: './w-base.component.html',
  styleUrls: ['./w-base.component.css']
})
export class WorkspaceBaseComponent {

  currentUser: UserDTO = {} as UserDTO;
  isExpanded: boolean = false;

  private unsubscribe$ = new Subject<void>();

  isShared: boolean = false;

  constructor(
    private registrationService: RegistrationService,
    private cacheResourceService: CacheResourceService,
    private route: ActivatedRoute
  ) {
    this.route.params.subscribe(async (params) => {
      await this.cacheResourceService.getUser().then(resp => {
        this.currentUser = resp as UserDTO
      });
      if(params['id']) {
        this.isShared = true;
      } else {
        this.isShared = false;
      }
    })
  }

  private getAutorithedUser() {
    return this.cacheResourceService
      .getUser();
  }

  public openSidenav() {
    this.isExpanded = true;
  }

  public closeSidenav() {
    this.isExpanded = false;
  }
}
