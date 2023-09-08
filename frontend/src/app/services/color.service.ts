import { Injectable } from '@angular/core';
import { hexToCSSFilter, HexToCssConfiguration } from '../services/color.services/index';

@Injectable({
    providedIn: 'root',
})

export class ColorService{

    constructor() {}

    hexToCss(color:string) {
        return hexToCSSFilter(color,  { forceFilterRecalculation: false } as HexToCssConfiguration);
    }
}