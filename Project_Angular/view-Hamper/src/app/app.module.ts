import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { HampersComponent } from './hampers/hampers.component';
const appRoutes: Routes = [
  {
    path: 'hampers',
    component: HampersComponent,
    data: { title: 'Hamper List' }
  },
  {
    path: '',
    redirectTo: '/hampers',
    pathMatch: 'full'
  }
];

@NgModule({
  declarations: [
    AppComponent,
    HampersComponent,
  ],
  imports: [
    RouterModule.forRoot(appRoutes),
    BrowserModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
