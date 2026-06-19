export interface EmployeeSummary {
    employeeId: string;
    name: string;
    totalTickets: number;
}
export interface TicketReport {
    startDate: string;
    endDate: string;
    totalTickets: number;
    totalEmployees: number;
    employees: EmployeeSummary[];
}
