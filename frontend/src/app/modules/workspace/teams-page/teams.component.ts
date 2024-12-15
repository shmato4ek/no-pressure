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
import { Router } from '@angular/router';
import { AddTeamDilogComponent } from '../add-team-dialog/add-team-dialog.component';
import { NewTeam } from 'src/app/models/team/new-team';
import { CacheResourceService } from 'src/app/services/cache.resource.service';

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html',
  styleUrls: ['./teams.component.css']
})
export class TeamsComponent implements OnInit{
  currentUser = {} as UserDTO;
  teams = [] as Team[];

  constructor(
    private registrationService: RegistrationService,
    private teamService: TeamService,
    private router: Router,
    public dialog: MatDialog,
    public cacheResourceService: CacheResourceService
  ) { }

  async ngOnInit(): Promise<void> {
    await this.cacheResourceService
      .getUser()
      .then(resp => {
        this.currentUser = resp as UserDTO;
      });
    this.teamService
      .getUsersTeams()
      .subscribe((teams) => {
        this.teams = teams;
      });
  }

  setUsersLine(team: Team) {
    let usersLine = ''
    
    if (!team.users) {
      return;
    }
    
    if(team?.users.length == 0) {
      return '';
    }

    if(team.users.length <= 3) {
      if(team.users.length > 1) {
        team.users.slice(0, team.users.length - 1).forEach(user => {
          usersLine += `${user.name}, `;
        });
      }

      usersLine += `${team.users[team.users.length-1].name}`;
    }

    else {
      usersLine = `${team.users[0].name}, ${team.users[1].name} and ${team.users.length - 2} more`;
    }

    return usersLine;
  }

  public showCreateTeamDialog() {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.autoFocus = true;
    
    dialogConfig.data = this.currentUser.id;

    const dialogRef = this.dialog.open(AddTeamDilogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe((team) => {
      if (team) {
        this.createTeam(team);
      }
    })
  }

  createTeam(team: NewTeam) {
    this.teamService.createTeam(team);
    window.location.reload();
  }

  getTeamTags(team: Team) {
    if (team.tags) {
      return team.tags.length > 3 ? team.tags.slice(0,3) : team.tags;
    } else {
      return null;
    }
  }

  redirectToTeam(team: Team) {
    this.router.navigate([`/teams/${team.uniqId}`]);
  }
}
