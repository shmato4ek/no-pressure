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
import { TeamService } from 'src/app/services/team.service';
import { UserDTO } from 'src/app/models/user/user-dto';
import { Team } from 'src/app/models/team/team';
import { ActivatedRoute } from '@angular/router';
import { ScheduleTime } from 'src/app/models/schedule/schedule-time';
import { ScheduleService } from 'src/app/services/schedule.service';

@Component({
  selector: 'app-team',
  templateUrl: './team.component.html',
  styleUrls: ['./team.component.css']
})
export class TeamComponent implements OnInit{
  currentUser = {} as UserDTO;
  author = {} as UserDTO;
  team = {} as Team;
  date = {} as string;
  firstHalfHours = [] as ScheduleTime[];
  secondHalfHours = [] as ScheduleTime[];

  constructor(
    private registrationService: RegistrationService,
    private teamService: TeamService,
    private activateRoute: ActivatedRoute,
    private scheduleService: ScheduleService,
  ) { }

  ngOnInit(): void {
    this.registrationService
      .getUser()
      .subscribe((user) => {
        this.currentUser = user;
        this.teamService
          .getTeamByUniqId(this.activateRoute.snapshot.params['id'])
          .subscribe((team) => {
            this.team = team;
            this.scheduleService
              .getTeamSchedule(this.team.id)
              .subscribe((schedule) => {
                this.firstHalfHours = schedule.hours.slice(0, 9),
                this.secondHalfHours = schedule.hours.slice(9, 18),
                this.date = schedule.date
              })
          })
      })
  }
  
  setBorderColor(activity?: ActivityDTO) {
    return activity?.color;
  }
}
