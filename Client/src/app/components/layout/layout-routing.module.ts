import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './layout.component';

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
            { path: 'employee', loadChildren: './employee/employee.module#EmployeeModule' },
            { path: 'farmer', loadChildren: './farmer/farmer.module#FarmerModule' },
            
            
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class LayoutRoutingModule {}
