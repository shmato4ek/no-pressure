import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { WorkSpaceModule } from './modules/workspace/workspace.module';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    WorkSpaceModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
