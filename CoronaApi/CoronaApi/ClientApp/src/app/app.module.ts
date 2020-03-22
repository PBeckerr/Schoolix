import { AccordionModule } from 'primeng/accordion';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AppComponent } from './app.component';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { CourseComponent } from './course/course.component';
import { CoursesComponent } from './courses/courses.component';
import { ExercisesComponent } from './exercises/exercises.component';
import { SubjectComponent } from "./subject/subject.component";
import { SubjectsComponent } from "./subjects/subjects.component";
import { FileUploadModule } from 'primeng/fileupload';
import { FormsModule } from '@angular/forms';
import { HomeComponent } from './home/home.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { NgModule } from '@angular/core';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CourseComponent,
    CoursesComponent,
    ExercisesComponent,
    SubjectComponent,
    SubjectsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'course/:id', component: CourseComponent },
      { path: 'subject/:id', component: SubjectComponent }
    ]),
    CardModule,
    ProgressSpinnerModule,
    AccordionModule,
    ButtonModule,
    FileUploadModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
