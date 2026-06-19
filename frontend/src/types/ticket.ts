export interface MealTicket {
    id: string;
    employeeId: string;
    employeeName: string;
    quantity: number;
    status: 'Active' | 'Inactive';
    deliveredAt: string;
}
export interface CreateTicketRequest {
    employeeId: string;
    quantity: number;
}
export interface UpdateTicketRequest {
    quantity: number;
    status: 'Active' | 'Inactive';
}
