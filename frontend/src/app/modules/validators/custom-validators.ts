import { ValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';
import { ScheduleHour } from 'src/app/models/enums/ScheduleHour';
import { ScheduleTime } from 'src/app/models/schedule/schedule-time';

export class CustomValidators {

    static passwordMatch(form: AbstractControl) {
        const password = form.get('password')?.value;
        const confirmPassord = form.get('confirmPassword')?.value;
        if (password !== confirmPassord)
        {
            form.get('confirmPassword')?.setErrors({noPasswordMatch: true})
        }
    }
    
    static schedullingValidation(form: AbstractControl) {
        const startTime = form.get('startTime')?.value;
        const endTime = form.get('endTime')?.value;
        if (startTime >= endTime)
        {
            form.get('endTime')?.setErrors({noTimeMatch: true})
        }
    }

    static checkboxValidation1(form: AbstractControl) {
        const checkbox1 = form.get('checkbox11')?.value;
        const checkbox2 = form.get('checkbox12')?.value;
        const checkbox3 = form.get('checkbox13')?.value;

        if(!checkbox1 && !checkbox2 && !checkbox3) {
            form.get('checkbox11')?.setErrors({noChosenCheckbox: true});
        }
    }

    static checkboxValidation2(form: AbstractControl) {
        const checkbox1 = form.get('checkbox21')?.value;
        const checkbox2 = form.get('checkbox22')?.value;
        const checkbox3 = form.get('checkbox23')?.value;

        if(!checkbox1 && !checkbox2 && !checkbox3) {
            form.get('checkbox21')?.setErrors({noChosenCheckbox: true})
        }
    }
}


export const regExps: { [key: string]: RegExp } = {
    password: /^(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{7,15}$/
};

/**
 * Collection of reusable error messages
 */
export const errorMessages: { [key: string]: string } = {
    fullName: 'Full name must be between 1 and 128 characters',
    email: 'Email must be a valid email address (username@domain)',
    confirmEmail: 'Email addresses must match',
    password: 'Password must be between 7 and 15 characters, and contain at least one number and special character',
    confirmPassword: 'Passwords must match'
};