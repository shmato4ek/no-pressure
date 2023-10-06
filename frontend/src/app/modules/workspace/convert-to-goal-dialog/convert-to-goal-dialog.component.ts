import { Component, Inject, Input, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { PlanDTO } from '../../../models/plan/plan-dto';
import { UpdatePlanDTO } from 'src/app/models/plan/plan-update';
import { PlansComponent } from '../plans/plans.component';
import { PlanService } from 'src/app/services/plan.service';
import { GoalDTO } from 'src/app/models/plan/goal-dto';
import { NewTag } from 'src/app/models/tag/new-tag';
import { NewActivity } from 'src/app/models/activity/new-activity';
import { ActivityForm } from 'src/app/models/activity/activity-form';

@Component({
  selector: 'convert-to-goal-dialog',
  templateUrl: './convert-to-goal-dialog.component.html',
  styleUrls: ['./convert-to-goal-dialog.component.css']
})
export class ConvertToGoalDialog implements OnInit{
  dialogForm: FormGroup = {} as FormGroup;
  plan: PlanDTO;
  tagColor = "#FFA500";

  constructor(
    private planService: PlanService,
    private dialogRef: MatDialogRef<PlansComponent>,
    private formBuilder: FormBuilder,
    public dialog: MatDialog,
    @Inject(MAT_DIALOG_DATA) public data: PlanDTO) {
      this.plan = data;
    }

    ngOnInit(): void {
      this.dialogForm = this.formBuilder.group({
        goalName: [,{
          validators: [
            Validators.required,
          ],
          updateOn:'change',
        }],
        activities: this.formBuilder.array([]),
        goalTag: [,{
          validators: [
            Validators.required,
          ],
          updateOn:'change',
        }

        ]
      });
      this.dialogForm.get('goalName')?.setValue(this.plan.name);
      this.dialogForm.get('goalTag')?.setValue(this.plan.name);
    }

    get activities() {
      return <FormArray>this.dialogForm.controls['activities'];
    }

    addActivity() {
      const activityForm = this.formBuilder.group({
          name: ['', Validators.required],
          description: ['', ],
      });
      this.activities.push(activityForm);
    }

    deleteActivity(activityIndex: number) {
      this.activities.removeAt(activityIndex);
    }

    save() {
      let newTag: NewTag = {
        userId: this.plan.userId,
        name: this.dialogForm.value.goalTag,
        color: (<HTMLInputElement>document.getElementById("colorpicker")).value
      }

      console.log(newTag);

      console.log(<FormArray>this.dialogForm.controls['activities'].value[0].name);

      var createdActivities = [] as ActivityForm[];

      for(let activity of this.activities.value)
      {
        let activityForm: ActivityForm = {
          name: activity.name,
          description: activity.description
        }

        createdActivities.push(activityForm);
      }

      let newActivities = [] as NewActivity[];

      for(let activity of createdActivities) {
        let newActivity: NewActivity = {
          userId: this.plan.userId,
          name: activity.name,
          description: activity.description,
          tag: newTag.name,
          color: newTag.color,
          isRepeatable: false
        }

        newActivities.push(newActivity);
      }

      let goal: GoalDTO = {
        id: this.plan.id,
        userId: this.plan.userId,
        name: this.dialogForm.value.goalName,
        tag: newTag,
        activities: newActivities
      }

      this.dialogRef.close(goal);
    }

    delete() {
      this.planService.deletePlan(this.plan.id);
      window.location.reload();
    }
}
