import api from './api';
import { Employee, CreateEmployeeRequest, UpdateEmployeeRequest } from '@/types/employee';
export const employeeService = {
    async getAll(): Promise<Employee[]> {
        const { data } = await api.get<Employee[]>('/api/employees');
        return data;
    },

    async create(request: CreateEmployeeRequest): Promise<void> {
        await api.post('/api/employees', request);
    },

    async update(id: string, request: UpdateEmployeeRequest): Promise<void> {
        await api.put(`/api/employees/${id}`, request);
    },
};
