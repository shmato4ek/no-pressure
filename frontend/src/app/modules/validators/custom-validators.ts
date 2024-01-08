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