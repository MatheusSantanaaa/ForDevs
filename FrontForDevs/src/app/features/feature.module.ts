import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FeatureRoutingModule } from './feature-routing.module';
import { FeatureComponent } from './feature.component';
import { LayoutModule } from '../layout/layout.module';

@NgModule({
  declarations: [
    FeatureComponent,
  ],
  imports: [CommonModule, FeatureRoutingModule, LayoutModule],
})
export class FeatureModule {}
