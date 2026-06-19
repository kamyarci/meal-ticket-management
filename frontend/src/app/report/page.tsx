'use client';

import {useState} from 'react';
import {Search} from 'lucide-react';
import {Button} from '@/components/ui/button';
import {Input} from '@/components/ui/input';
import {Table, TableBody, TableCell, TableHead, TableHeader, TableRow} from '@/components/ui/table';
import StatCard from '@/components/StatCard';
import {reportService} from '@/services/reportService';
import {TicketReport} from '@/types/report';
import {buildDateTime} from '@/utils/format';
import {useAsync} from '@/hooks/useAsync';

export default function ReportPage() {
    const [startDate, setStartDate] = useState('');
    const [startTime, setStartTime] = useState('');
    const [endDate, setEndDate] = useState('');
    const [endTime, setEndTime] = useState('');
    const [report, setReport] = useState<TicketReport | null>(null);
    const {loading, error, setError, run} = useAsync();

    async function handleSearch() {
        if (!startDate || !endDate) {
            setError('Informe as datas de início e fim.');
            return;
        }
        await run(async () => {
            const start = buildDateTime(startDate, startTime, false);
            const end = buildDateTime(endDate, endTime, true);
            const data = await reportService.getReport(start, end);
            setReport(data);
        });
    }

    return (
        <div className="space-y-6">
            <h1 className="text-2xl font-bold">Relatório</h1>

            <div className="border rounded-lg p-4 space-y-4">
                <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
                    <div className="space-y-1">
                        <label className="text-sm font-medium">Data inicial</label>
                        <Input type="date" value={startDate} onChange={e => setStartDate(e.target.value)}/>
                    </div>
                    <div className="space-y-1">
                        <label className="text-sm font-medium">Hora inicial (opcional)</label>
                        <Input type="time" value={startTime} onChange={e => setStartTime(e.target.value)}/>
                    </div>
                    <div className="space-y-1">
                        <label className="text-sm font-medium">Data final</label>
                        <Input type="date" value={endDate} onChange={e => setEndDate(e.target.value)}/>
                    </div>
                    <div className="space-y-1">
                        <label className="text-sm font-medium">Hora final (opcional)</label>
                        <Input type="time" value={endTime} onChange={e => setEndTime(e.target.value)}/>
                    </div>
                </div>

                {error && <p className="text-sm text-red-500">{error}</p>}

                <Button onClick={handleSearch} disabled={loading} className="w-full sm:w-auto">
                    <Search className="h-4 w-4"/>
                    {loading ? 'Buscando...' : 'Buscar'}
                </Button>
            </div>
            {report && (
                <div className="space-y-4">
                    <div className="grid grid-cols-2 gap-4">
                        <StatCard label="Total de Tickets" value={report.totalTickets}/>
                        <StatCard label="Funcionários" value={report.totalEmployees}/>
                    </div>
                    <div className="rounded-md border">
                        <Table>
                            <TableHeader>
                                <TableRow>
                                    <TableHead>Funcionário</TableHead>
                                    <TableHead className="text-right">Tickets</TableHead>
                                </TableRow>
                            </TableHeader>
                            <TableBody>
                                {report.employees.map(emp => (
                                    <TableRow key={emp.employeeId}>
                                        <TableCell>{emp.name}</TableCell>
                                        <TableCell className="text-right">{emp.totalTickets}</TableCell>
                                    </TableRow>
                                ))}
                            </TableBody>
                        </Table>
                    </div>
                </div>
            )}
        </div>
    );
}
