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

@Component({
  selector: 'app-goals',
  templateUrl: './goals.component.html',
  styleUrls: ['./goals.component.css']
})
export class GoalsComponent implements OnInit{
  userId = {} as number;
  goals = [] as GoalInfoDTO[];
  public isAppear = false;

  currentlyChecked = 0;
  currentGoal = {} as GoalInfoDTO;

  constructor(
    private registrationService: RegistrationService,
    private planService: PlanService,
  ) { }

  ngOnInit(): void {
    this.getUserId();

    setTimeout(() => {
      this.showStat();
    }, 100)
  }
  showStat() {
    this.isAppear = true;
  }

  public getUserId() {
    this.registrationService
      .getUser()
      .subscribe((user) => {
        this.userId = user.id;
        this.planService
          .getAllGoals(user.id)
          .subscribe((goals) => {
            this.goals = goals;
          });
      });
  }
  
  selectCheckBox(goal: GoalInfoDTO) {
    if(this.currentlyChecked === goal.id) {
      this.currentlyChecked = 0;
      this.currentGoal = {} as GoalInfoDTO;
      return;
    }

    this.currentlyChecked = goal.id;
    this.currentGoal = goal;
  }

  checkActivities(activities: ActivityDTO[]) {
    if(activities.length === 0) {
      return false
    } else {
      console.log(activities)
      return true;
    }
  }

}
