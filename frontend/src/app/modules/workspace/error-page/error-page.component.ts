import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Subject, takeUntil } from 'rxjs';
import { PlanState } from 'src/app/models/enums/PlanState';
import { NewPlanDTO } from 'src/app/models/plan/new-plan';
import { PlanChangeState } from 'src/app/models/plan/plan-change-state';
import { PlanDTO } from 'src/app/models/plan/plan-dto';
import { PlanService } from 'src/app/services/plan.service';
import { RegistrationService } from 'src/app/services/registration.service';
import { PlanEditDialogComponent } from '../plan-edit-dialog/plan-edit-dialog.component';
import { UpdatePlanDTO } from 'src/app/models/plan/plan-update';
import { ConvertToGoalDialog } from '../convert-to-goal-dialog/convert-to-goal-dialog.component';
import { GoalDTO } from 'src/app/models/plan/goal-dto';
import { GoalInfoDTO } from 'src/app/models/plan/goal-info-dto';
import { ActivityDTO } from 'src/app/models/activity/activity-dto';
import { CacheResourceService } from 'src/app/services/cache.resource.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-error',
  templateUrl: './error-page.component.html',
  styleUrls: ['./error-page.component.css']
})
export class ErrorPageComponent implements OnInit{
  imgSrc = '';
  isAuthorized = true;

  constructor(private activateRoute: ActivatedRoute, private router: Router) {
  }

  ngOnInit(): void {
      this.setErrorImg(this.activateRoute.snapshot.params['status']);
  }

  setErrorImg(status: string) {
    var statusCode: number = +status;
    switch (statusCode) {
      case 500: 
        this.imgSrc = '../../../../assets/img/error-500.png';
        break;
      case 404: 
        this.imgSrc = '../../../../assets/img/error-404.png'
        break;
      case 403:
        this.imgSrc = '../../../../assets/img/error-no-access.png'
        break;
      case 401:
        this.imgSrc = '../../../../assets/img/error-401.png'
        this.isAuthorized = false;
        break;
      default:
        this.imgSrc = '../../../../assets/img/error-404.png';
        break;
    }
  }

  redirectToLogin() {
    console.log("1")
    this.router.navigate(['auth']);
  }
}
