import { Component, Inject, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { PlanDTO } from '../../../models/plan/plan-dto';
import { UpdatePlanDTO } from 'src/app/models/plan/plan-update';
import { PlansComponent } from '../plans/plans.component';
import { PlanService } from 'src/app/services/plan.service';

@Component({
  selector: 'plan-edit-dialog',
  templateUrl: './plan-edit-dialog.component.html',
  styleUrls: ['./plan-edit-dialog.component.css']
})
export class PlanEditDialogComponent implements OnInit{
  dialogForm: FormGroup = {} as FormGroup;
  plan: PlanDTO;

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
        planName: [,{
          validators: [
            Validators.required,
          ],
          updateOn:'change',
        }],
      });
      this.dialogForm.get('planName')?.setValue(this.plan.name);
    }

    save() {
      let plan: UpdatePlanDTO = {
        id: this.plan.id,
        name: this.dialogForm.value.planName,
      }

      this.dialogRef.close(plan);
    }

    delete() {
      this.planService.deletePlan(this.plan.id);
      window.location.reload();
    }
}
