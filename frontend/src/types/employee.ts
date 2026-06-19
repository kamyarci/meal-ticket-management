export interface Employee {
    id: string;
    name: string;
    cpf: string;
    status: 'Active' | 'Inactive';
    updatedAt: string;
}
export interface CreateEmployeeRequest {
    name: string;
    cpf: string;
}
export interface UpdateEmployeeRequest {
    name: string;
    status: 'Active' | 'Inactive';
}
