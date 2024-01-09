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
import { SnackBarService } from 'src/app/services/snack-bar.service';

@Component({
  selector: 'app-plans',
  templateUrl: './plans.component.html',
  styleUrls: ['./plans.component.css']
})
export class PlansComponent implements OnInit{
  public userId = {} as number;
  public plans = [] as PlanDTO[];
  private unsubscribe$ = new Subject<void>();
  public isEditing = false as boolean;
  dialogForm: FormGroup = {} as FormGroup;

  constructor(
    private snackBarService: SnackBarService,
    private registrationService: RegistrationService,
    private planService: PlanService,
    public dialog: MatDialog,
    private formBuilder: FormBuilder,
  ) { }

  ngOnInit(): void {
      this.getUserId();
      this.dialogForm = this.formBuilder.group({
        planName: [,{
          validators: [
            Validators.required,
            Validators.maxLength(30),
          ],
          updateOn:'change',
        }],
      });
  }

  public getUserId() {
    this.registrationService
      .getUser()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((user) => {
        this.userId = user.id;
        this.planService
          .getAllNoGoalActivities(user.id)
          .subscribe((plans) => {
            this.plans = plans;
          });
      });
  }

  public switchEditMode() {
    this.isEditing = !this.isEditing;
  }

  public save() {
    let plan: NewPlanDTO = {
      name: this.dialogForm.value.planName,
      userId: this.userId
    }

    this.planService.createPlan(plan);

    window.location.reload();
  }

  public showEditPlan(plan: PlanDTO) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.autoFocus = true;
    dialogConfig.data = plan;

    const dialogRef = this.dialog.open(PlanEditDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe((plan
      ) => {
      let updatedPlan: UpdatePlanDTO = {
        id: plan.id,
        name: plan.name
      }
      this.planService.update(updatedPlan).subscribe();
      window.location.reload();
    })
  }

  public showConvertDialog(plan: PlanDTO) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.autoFocus = true;
    dialogConfig.data = plan;

    const dialogRef = this.dialog.open(ConvertToGoalDialog, dialogConfig);

    dialogRef.afterClosed().subscribe((goal
      ) => {
      let newGoal: GoalDTO = {
        id: goal.id,
        userId: goal.userId,
        name: goal.name,
        tag: goal.tag,
        activities: goal.activities
      }
      this.planService.convertToGoal(newGoal);
      window.location.reload();
    })
  }

  inputValidation(event: any, target: string) {   
    var k;  
    k = event.charCode;
    var isValid = ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || (k >= 48 && k <= 57));
    if (!isValid) {
      this.openSnackBar(target);
    }
    return isValid; 
  }

  openSnackBar(target: string) {
    this.snackBarService.openSnackBar(`${target} must contain only latin symbols!`);
  }
}
