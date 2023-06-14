import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { LayoutModule } from './layout/layout.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { httpInterceptorsProviders } from './core';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { FeatureComponent } from './features/feature.component';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    LayoutModule,
    FormsModule,
    ReactiveFormsModule,
    ToastrModule.forRoot({}),
    BrowserAnimationsModule,
    HttpClientModule,
    NgbModalModule
  ],
  providers: [
    httpInterceptorsProviders
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
