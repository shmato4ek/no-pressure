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
import { SnackBarService } from 'src/app/services/snack-bar.service';
import { TagService } from 'src/app/services/tag.service';
import { TagInfo } from 'src/app/models/tag/tag-info';

@Component({
  selector: 'convert-to-goal-dialog',
  templateUrl: './convert-to-goal-dialog.component.html',
  styleUrls: ['./convert-to-goal-dialog.component.css']
})
export class ConvertToGoalDialog implements OnInit{
  dialogForm: FormGroup = {} as FormGroup;
  plan: PlanDTO;
  tagColor = "#FFA500";
  tags = [] as TagInfo[];

  isTagAvailable = true;

  constructor(
    private snackBarService: SnackBarService,
    private planService: PlanService,
    private tagService: TagService,
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
            Validators.maxLength(15)
          ],
          updateOn:'change',
        }],
        activities: this.formBuilder.array([]),
        goalTag: [,{
          validators: [
            Validators.required,
            Validators.maxLength(15)
          ],
          updateOn:'change',
        }

        ]
      });
      this.dialogForm.get('goalName')?.setValue(this.plan.name);
      this.dialogForm.get('goalTag')?.setValue(this.plan.name);

      this.tagService.getAllTagsInfo(this.plan.userId)
        .subscribe((resp) => {
          this.tags = resp;
        });
    }

    get activities() {
      return <FormArray>this.dialogForm.controls['activities'];
    }

    test() {
      let a = this.dialogForm.controls['activities'].get(['0'])?.errors?.['required'];
      console.log(a);
    }

    addActivity() {
      const activityForm = this.formBuilder.group({
          name: [,{
            validators: [
              Validators.required,
              Validators.maxLength(15),
            ],
            updateOn:'change',
          }],
          description: [,{
            validators: [
              Validators.maxLength(15),
            ],
            updateOn:'change',
          }],
      });
      this.activities.push(activityForm);
    }

    deleteActivity(activityIndex: number) {
      this.activities.removeAt(activityIndex);
    }
    
    validateTag() {
      setTimeout(() => {
        this.tagValidation()
      }, 100)
    }

    tagValidation() {
      let tag = this.dialogForm.get(['goalTag'])?.value;

      this.tags.forEach(t => {
        if (t.name == tag) {
          this.isTagAvailable = false;
        }
      });
    }

    save() {
      let newTag: NewTag = {
        userId: this.plan.userId,
        name: this.dialogForm.value.goalTag,
        color: (<HTMLInputElement>document.getElementById("colorpicker")).value
      }

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
          isRepeatable: false,
          teamId: 0,
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

    inputValidation(event: any, target: string) {   
      var k;  
      k = event.charCode;
      var isValid = (
        (k > 64 && k < 91) || 
        (k > 96 && k < 123) ||
        k == 8 ||
        k == 32 ||
        (k >= 48 && k <= 57) ||
        (k >= 33 && k <= 47) ||
        (k >= 58 && k <= 64) ||
        (k >= 91 && k <= 96) ||
        (k >= 123 && k <= 126));
      if (!isValid) {
        this.openSnackBar(target);
      }
      return isValid; 
    }

    openSnackBar(target: string) {
      this.snackBarService.openSnackBar(`${target} must contain only latin symbols!`);
    }

    close() {
      this.dialogRef.close();
    }
}
