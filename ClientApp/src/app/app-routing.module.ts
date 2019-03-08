import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DashboardComponent } from './dashboard/dashboard.component';
import { LoginComponent } from './employee/login/login.component';
import { RegistrationComponent } from './employee/registration/registration.component';
import { ForgetPasswordComponent } from './employee/forget-password/forget-password.component';
import { EmployeedetailComponent } from './employee/employeedetail/employeedetail.component';
import { CategoryComponent } from './post/category/category.component';
import { PostListComponent } from './post-list/post-list.component';
import { PostComponent } from './post/post.component';
import { PostDetailComponent } from './post/post-detail/post-detail.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { AuthGuard } from './AuthGaurd/auth-guard.service';
import { PostActivityTypeListComponent } from './post/post-activityType-list/post-activityType-list.component';


const routes: Routes = [
  { path: '', component: LoginComponent, pathMatch: 'prefix' },
  { path: 'register', component: RegistrationComponent },
  { path: 'forgetpassword', component: ForgetPasswordComponent },
  { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] },
  { path: 'postactivitytype/:type', component: PostActivityTypeListComponent, canActivate: [AuthGuard] },
  { path: 'empdetail/:id', component: EmployeedetailComponent, canActivate: [AuthGuard] },  
  { path: 'post-list', component: PostListComponent,canActivate: [AuthGuard] },  
  { path: 'postdetail/:id', component: PostDetailComponent, canActivate: [AuthGuard] },
  { path: '**', component: PageNotFoundComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

export const routingComponents = [LoginComponent, RegistrationComponent, ForgetPasswordComponent, DashboardComponent, PostComponent, PostDetailComponent, PostListComponent, PostActivityTypeListComponent,EmployeedetailComponent, CategoryComponent, PageNotFoundComponent]
