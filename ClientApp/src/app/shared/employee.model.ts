export class Employee {
    EmployeeId : number;
    FirstName :string;
    LastName :string;
    FullName : string;
    UserName :string;
    Password : string;
    ConfirmPassword: string;
    Email:string;
    photo?:string;
    MobileNo:string;
    Gender :string;
    CreatedDate:Date;
    LastLoggedInDate:Date;
    message?:string;
    isExists? : boolean;
}
