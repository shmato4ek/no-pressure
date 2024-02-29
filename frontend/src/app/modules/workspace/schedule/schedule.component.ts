import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Subject, takeUntil } from 'rxjs';
import { ActivityDTO } from 'src/app/models/activity/activity-dto';
import { ActivityService } from 'src/app/services/activity.service';
import { RegistrationService } from 'src/app/services/registration.service';
import { TaskAddDialogComponent } from '../task-adding-dialog/task-add-dialog.component';
import { NewActivity } from 'src/app/models/activity/new-activity';
import { UpdateActivity } from 'src/app/models/activity/update-activity';
import { TaskDialogComponent } from '../task-dialog/task-dialog.component';
import { Schedule } from 'src/app/models/schedule/schedule';
import { ScheduleService } from 'src/app/services/schedule.service';
import { ScheduleHour } from 'src/app/models/enums/ScheduleHour';
import { ScheduleTime } from 'src/app/models/schedule/schedule-time';
import { Tag } from 'src/app/models/tag/tag';
import { ColorService } from 'src/app/services/color.service';
import { ScheduledTaskDialogComponent } from '../scheduled-task-dialog/scheduled-task-dialog.component';
import { TaskScheduleDialogComponent } from '../task-schedule-dialog/task-schedule-dialog-component';
import { AddTaskToSchedule } from 'src/app/models/schedule/add-task-to-schedule';
import { TagEditDialogComponent } from '../tag-edit-dialog/tag-edit-dialog.component';
import { UpdateTag } from 'src/app/models/tag/update-tag';
import { TagService } from 'src/app/services/tag.service';
import { NewActivityInfo } from 'src/app/models/activity/new-activity-info';
import { TeamInfo } from 'src/app/models/team/team-info';
import { TeamAccess } from 'src/app/models/enums/TeamAccess';
import { CacheResourceService } from 'src/app/services/cache.resource.service';
import { UserDTO } from 'src/app/models/user/user-dto';
import { TeamScheduleOption } from 'src/app/models/team/team-schedule-option';
import { TooltipPosition } from '@angular/material/tooltip';
import { ScheduleActivity } from 'src/app/models/activity/schedule-activity';
import { ScheduledActivity } from 'src/app/models/activity/scheduled-activity';

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.css']
})
export class ScheduleComponent implements OnInit{
  public currentUser = {} as UserDTO;
  public tags = [] as Tag[];
  public firstHalfHours = [] as ScheduleTime[];
  public secondHalfHours = [] as ScheduleTime[];
  public date = {} as string;
  public teams = [] as TeamInfo[];
  public selectedSchedule = {} as TeamScheduleOption;
  public teamsOptions = [] as TeamScheduleOption[];
  public allScheduledActivities = [] as ScheduleTime[];
  positionOption: TooltipPosition = 'below';

  _no_team_tooltip = "Here you will see a list of commands where you can edit the schedule";

  public separator = 14 as number;
  public isEditing = false as boolean;
  public hovered1 = -1;
  public hovered2 = -1;

  public dropdownSize = 5 as number;

  private unsubscribe$ = new Subject<void>();

  constructor(
    private registrationService: RegistrationService,
    private activityService: ActivityService,
    private colorService: ColorService,
    private scheduleService: ScheduleService,
    private cacheResourceService: CacheResourceService,
    private tagService: TagService,
    public dialog: MatDialog
  ) { }

  async ngOnInit(): Promise<void> {
      await this.getUserId();
      let page = this.cacheResourceService.getSchedulePage();
      let team = this.teamsOptions.find(t => t.name == page);

      if (team != undefined) {
        this.selectedSchedule = {
          name: page.toString(),
          id: team.id        
        }
      }
  }

  setActivityStyle(activity: ActivityDTO) {
      return `2px solid ${activity.color}`
  }

  public dropDownChoose() {
    if (this.selectedSchedule.id == 0)
    {
      this.getPersonalSchedule(this.currentUser.id);
    } else {
      this.getTeamSchedule(this.selectedSchedule.id);
    }

    this.cacheResourceService.setSchedulePage(this.selectedSchedule.name);
  }

  public async getUserId() {
    await this.cacheResourceService
      .getUser()
      .then(resp => {
        this.currentUser = resp as UserDTO;
      });
      if (this.currentUser.teams) {
        this.teams = this.setTeams(this.currentUser.teams);

        let personal: TeamScheduleOption = {
          id: 0,
          name: "Personal",
        }
        
        this.teamsOptions.push(personal);
        this.selectedSchedule = personal;
        this.teams.forEach(team => {
          let newTeam: TeamScheduleOption = {
            id: team.id,
            name: team.name,
          }
          this.teamsOptions.push(newTeam);
        });
      };
      this.getPersonalSchedule(this.currentUser.id);
  }

  async getTeamSchedule(teamId: number) {
    await this.cacheResourceService.getSchedule(teamId)
      .then((schedule) => {
        if (schedule != undefined) {
          this.tags = schedule.tags,
          this.firstHalfHours = schedule.hours.slice(0, 9),
          this.secondHalfHours = schedule.hours.slice(9, 18),
          this.date = schedule.date,
          this.allScheduledActivities = schedule.hours
        }
      })
  }

  getPersonalSchedule(userId: number) {
    this.scheduleService.getSchedule(this.currentUser.id)
      .subscribe((schedule) => {
        this.tags = schedule.tags,
        this.firstHalfHours = schedule.hours.slice(0, 9),
        this.secondHalfHours = schedule.hours.slice(9, 18),
        this.allScheduledActivities = schedule.hours,
        this.date = schedule.date
      })
  }

  getTags() {
    return this.tags;
  }

  setTeams(teams: TeamInfo[]) {
    let userTeams = [] as TeamInfo[];
    teams.forEach(team => {
      if (team.addingActivities == TeamAccess.Allow) {
        userTeams.push(team);
      }
    });
    return userTeams;
  }

  public showAddActivityDialog() {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.autoFocus = true;

    const newActivityInfo: NewActivityInfo = {
      userId: this.currentUser.id,
      teamId: 0,
    }

    if(this.selectedSchedule.id != 0) {
      newActivityInfo.teamId = this.selectedSchedule.id;
    }
    
    dialogConfig.data = newActivityInfo;

    const dialogRef = this.dialog.open(TaskAddDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe((activity) => {
      if (activity) {
        let newActivity: NewActivity = {
          userId: this.currentUser.id,
          name: activity.name,
          description: activity.description,
          tag: activity.tag,
          isRepeatable: activity.isRepeatable,
          color: activity.color,
          teamId: activity.teamId,
        };
        this.createActivity(newActivity);
      }
    })
  }

  public showActivityDialog(currentActivity: ActivityDTO) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.autoFocus = true;
    dialogConfig.data = currentActivity;

    const dialogRef = this.dialog.open(TaskDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe((activity) => {
      if (activity) {
        let updateActivity: UpdateActivity = {
          id: currentActivity.id,
          name: activity.name,
          description: activity.description,
          startTime: ScheduleHour.Undefined,
          endTime: ScheduleHour.Undefined,
        };
        this.updateActivity(updateActivity);
      }
    })
  }

  public showScheduleActivityDialog(currentActivity: ActivityDTO) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.autoFocus = true;
    
    let activity: ScheduleActivity = {
      name: currentActivity.name,
      id: currentActivity.id,
      description: currentActivity.description,
      schedule: this.allScheduledActivities
    }

    dialogConfig.data = activity;

    const dialogRef = this.dialog.open(TaskScheduleDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe((activity) => {
      if(activity) {
        let scheduledActivity: AddTaskToSchedule = {
          activityId: currentActivity.id,
          startTime: activity.startTime,
          endTime: activity.endTime,
        };
        this.scheduleActivity(scheduledActivity);
      }
    })
  }

  public showScheduledActivityDialog(currentActivity?: ActivityDTO) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.autoFocus = true;

    if (currentActivity) {

    let activity: ScheduledActivity = {
      id: currentActivity.id,
      name: currentActivity.name,
      description: currentActivity.description,
      schedule: this.allScheduledActivities,
      startTime: currentActivity.startTime,
      endTime: currentActivity.endTime,
      state: currentActivity.state
    }

    dialogConfig.data = activity;
    }

    const dialogRef = this.dialog.open(ScheduledTaskDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe((activity) => {
      if (activity) {
        if(currentActivity != null) {
          let updateActivity: UpdateActivity = {
            id: currentActivity.id,
            name: currentActivity.name,
            description: currentActivity.description,
            startTime: activity.startTime,
            endTime: activity.endTime
          };

          this.updateActivity(updateActivity);
        }
      }
    })
  }

  public showTagDialog(currentTag?: Tag) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.autoFocus = true;
    dialogConfig.data = currentTag;

    const dialogRef = this.dialog.open(TagEditDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe((tag) => {
      if (tag) {
        if(currentTag != null) {
        let updateTag: UpdateTag = {
          id: currentTag.id,
          name: tag.name,
          color: tag.color
        };

        this.updateTag(updateTag);
      }
    }
    })
  }

  updateTag(updateTag: UpdateTag) {
    this.tagService.updateTag(updateTag)
      .subscribe();
    window.location.reload();
  }

  public createActivity(newActivity: NewActivity) {
    this.activityService.add(newActivity)
      .subscribe();
    window.location.reload();
  }

  public updateActivity(updatedActivity: UpdateActivity) {
    this.activityService.update(updatedActivity)
      .subscribe();
    window.location.reload();
  }

  public scheduleActivity(activity: AddTaskToSchedule) {
    this.scheduleService.addActivityToSchedule(activity);
    window.location.reload();
  }

  setSvgColor(tag: Tag) {
    return this.colorService.hexToCss(tag.color).filter.replace(';', "");
  }

  setBorderColor(activity?: ActivityDTO) {
    return activity?.color;
  }

  changeIsEditingState() {
    this.isEditing = !this.isEditing;
  }
}
