import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './layout.component';
import { FarmerRoleGuard, ManagerRoleGuard } from '../shared/guard';
import { HumanresourcesRoleGuard } from '../shared/guard/humanresources-role.guard';

const routes: Routes = [
    {
        path: '',
        component: LayoutComponent,
        children: [
            { path: '', redirectTo: 'dashboard' },
            { path: 'dashboard', loadChildren: './dashboard/dashboard.module#DashboardModule' },
            { path: 'resource', loadChildren: './resource/resource.module#ResourceModule' },
            { path: 'imexport', loadChildren: './imexport/imexport.module#ImexportModule' },
            { path: 'land', loadChildren: './land/land.module#LandModule' },
            { path: 'document', loadChildren: './document/document.module#DocumentModule' },
            { path: 'employee', loadChildren: './employee/employee.module#EmployeeModule', canActivate : [HumanresourcesRoleGuard] },
            { path: 'farmer', loadChildren: './farmer/farmer.module#FarmerModule', canActivate : [HumanresourcesRoleGuard] },
            { path: 'account-info', loadChildren: './account-info/account-info.module#AccountInfoModule' },
            
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class LayoutRoutingModule {}
