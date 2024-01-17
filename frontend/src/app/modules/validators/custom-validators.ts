import { ValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';

export class CustomValidators {

    static passwordMatch(form: AbstractControl) {
        const password = form.get('password')?.value;
        const confirmPassord = form.get('confirmPassword')?.value;
        if (password !== confirmPassord)
        {
            form.get('confirmPassword')?.setErrors({noPasswordMatch: true})
        }
    }
    
    static checkboxValidation1(form: AbstractControl) {
        const checkbox1 = form.get('checkbox11')?.value;
        const checkbox2 = form.get('checkbox12')?.value;
        const checkbox3 = form.get('checkbox13')?.value;

        console.log(`1: ${checkbox1}, 2: ${checkbox2}, 3: ${checkbox3}`)

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