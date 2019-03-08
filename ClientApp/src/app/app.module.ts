import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule,routingComponents } from './app-routing.module';
import { AppComponent } from './app.component';
import {HttpClientModule,HTTP_INTERCEPTORS  } from '@angular/common/http';
import { httpInterceptor } from './Interceptor/httpInterceptor';
import { ErrorInterceptor } from './Interceptor/errorInterceptor';
import { LoginService } from './shared/login.service';
import { AuthGuard } from './AuthGaurd/auth-guard.service';

@NgModule({
  declarations: [
    AppComponent,
    routingComponents   
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
    
  ],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: httpInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    LoginService,AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
