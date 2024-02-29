import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HomeModule } from './modules/home/home.module';
import { AppRoutingModule } from './app-routing.module';
import { AuthModule } from './modules/auth/auth.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { TokenInterceptor } from './services/interceptor';
import { WorkspaceModule } from './modules/workspace/workspace.module';
import { IonicModule } from '@ionic/angular';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { CacheService } from './services/cache.service';
import { GoogleLoginProvider, SocialAuthServiceConfig, SocialLoginModule } from '@abacritt/angularx-social-login';
import { ErrorInterceptor } from './services/error-interceptor';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    HomeModule,
    AuthModule,
    BrowserAnimationsModule,
    WorkspaceModule,
    MatSnackBarModule,
    IonicModule.forRoot({}),
    SocialLoginModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true,
    },
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
          autologin: false,
          providers: [
              {
                  id: GoogleLoginProvider.PROVIDER_ID,
                  provider: new GoogleLoginProvider(
                      '587838485415-snc43mtvbkmbtf74g95reo1jo9focve4.apps.googleusercontent.com', {
                        oneTapEnabled: false
                      }
                  )
              }
          ],
          onError: (err) => {
              console.log(err);
          }
      } as SocialAuthServiceConfig,
  },
    CacheService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
