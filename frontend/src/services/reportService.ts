import api from './api';
import { TicketReport } from '@/types/report';
export const reportService = {
    async getReport(start: string, end: string): Promise<TicketReport> {
        const { data } = await api.get<TicketReport>('/api/mealtickets/report', {
            params: { startDate: start, endDate: end },
        });
        return data;
    },
};
