import api from './api';
import { MealTicket, CreateTicketRequest, UpdateTicketRequest } from '@/types/ticket';
export const ticketService = {
    async getAll(): Promise<MealTicket[]> {
        const { data } = await api.get<MealTicket[]>('/api/mealtickets');
        return data;
    },

    async create(request: CreateTicketRequest): Promise<void> {
        await api.post('/api/mealtickets', request);
    },

    async update(id: string, request: UpdateTicketRequest): Promise<void> {
        await api.put(`/api/mealtickets/${id}`, request);
    },
};
