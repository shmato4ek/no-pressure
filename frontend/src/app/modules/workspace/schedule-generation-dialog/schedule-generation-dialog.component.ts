import { Component, Inject, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ScheduleComponent } from '../schedule/schedule.component';
import { ScheduleGenerationConfiguration } from 'src/app/models/schedule/schedule-generation-configuration';
import { ScheduleService } from 'src/app/services/schedule.service';

@Component({
  selector: 'schedule-generation-dialog',
  templateUrl: './schedule-generation-dialog.component.html',
  styleUrls: ['./schedule-generation-dialog.component.css']
})
export class ScheduleGenerationDialogComponent implements OnInit{
  dialogForm: FormGroup = {} as FormGroup;

  config = {} as ScheduleGenerationConfiguration;

  constructor(
    private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<ScheduleComponent>,
    private scheduleService: ScheduleService) {

    }

    ngOnInit(): void {
      this.dialogForm = this.formBuilder.group({
        isCrossowerEnabled: [, {
          validators: []
        }],
        isMutationEnabled: [, {
          validators: []
        }],
        iterationsAmount: [, {
          validatirs: []
        }],
      });

      this.scheduleService.getScheduleConfig()
      .subscribe(
        (resp) => {
          this.config = resp
          this.setFormGroup(resp)
        }
      )
    }

    setFormGroup(data: ScheduleGenerationConfiguration) {
      this.dialogForm.get('isCrossowerEnabled')?.setValue(data.isCrossowerEnabled);
      this.dialogForm.get('isMutationEnabled')?.setValue(data.isMutationEnabled);
      this.dialogForm.get('iterationsAmount')?.setValue(data.iterationsAmount);
    }

    save() {
      let config: ScheduleGenerationConfiguration =
      {
        userId: this.config.userId,
        isCrossowerEnabled: this.dialogForm.get('isCrossowerEnabled')?.value,
        isMutationEnabled: this.dialogForm.get('isMutationEnabled')?.value,
        iterationsAmount: this.dialogForm.get("iterationsAmount")?.value
      }
      console.log(config)
      this.dialogRef.close(config);
    }

    close() {
      this.dialogRef.close();
    }
}
