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
import { ChangeGoalState } from 'src/app/models/plan/change-goal-state';
import { GoalState } from 'src/app/models/enums/GoalState';

@Component({
  selector: 'app-goals',
  templateUrl: './goals.component.html',
  styleUrls: ['./goals.component.css']
})
export class GoalsComponent implements OnInit{
  userId = {} as number;
  activeGoals = [] as GoalInfoDTO[];
  closedGoals = [] as GoalInfoDTO[];
  public isAppear = false;

  currentlyChecked = 0;
  currentGoal = {} as GoalInfoDTO;

  goalsOptions = [] as string[];
  currentOption = 'Active';

  _no_goals_tooltip = 'In order to have goals, create a plan and convert it to goal';

  constructor(
    private registrationService: RegistrationService,
    private planService: PlanService,
    private cacheResourceService: CacheResourceService
  ) { }

  ngOnInit(): void {
    this.getUserId();

    setTimeout(() => {
      this.showStat();
    }, 100)

    this.goalsOptions.push('Active');
    this.goalsOptions.push('Closed');
  }
  showStat() {
    this.isAppear = true;
  }

  getGoalStat() {
    var doneTasks = this.currentGoal.doneTasksAmmount;
    var allTasks = this.currentGoal.allTasksAmmount;
    if(allTasks == 0) {
      return 0;
    } else {
      return doneTasks/allTasks;
    }
  }

  async getUserId() {
    await this.cacheResourceService
    .getUser()
    .then(user => {
      if (user != undefined) {
        this.userId = user.id;
        this.planService
          .getAllGoals(user.id)
          .subscribe((goals) => {
            this.activeGoals = goals.activeGoals;
            this.closedGoals = goals.closedGoals;
          });
      }
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
      return true;
    }
  }

  closeGoal() {
    let goal: ChangeGoalState = {
      id: this.currentGoal.id,
      state: GoalState.Closed,
    }

    this.planService.changeGoalState(goal);

    window.location.reload();
  }

  makeGoalActive() {
    let goal: ChangeGoalState = {
      id: this.currentGoal.id,
      state: GoalState.Active,
    }

    this.planService.changeGoalState(goal);

    window.location.reload();
  }

}
