'use client';

import {useEffect, useState} from 'react';
import {Ticket, Pencil} from 'lucide-react';
import {Button} from '@/components/ui/button';
import {Input} from '@/components/ui/input';
import {Dialog, DialogContent, DialogHeader, DialogTitle} from '@/components/ui/dialog';
import {Select, SelectContent, SelectItem, SelectTrigger, SelectValue} from '@/components/ui/select';
import {Table, TableBody, TableCell, TableHead, TableHeader, TableRow} from '@/components/ui/table';
import StatCard from '@/components/StatCard';
import StatusBadge from '@/components/StatusBadge';
import FormField from '@/components/FormField';
import {ticketService} from '@/services/ticketService';
import {employeeService} from '@/services/employeeService';
import {MealTicket} from '@/types/ticket';
import {Employee} from '@/types/employee';
import {useFormModal} from '@/hooks/useFormModal';
import {useAsync} from '@/hooks/useAsync';

export default function TicketsPage() {
    const [tickets, setTickets] = useState<MealTicket[]>([]);
    const [employees, setEmployees] = useState<Employee[]>([]);
    const {loading, error: fetchError, run: loadData} = useAsync();
    const [formEmployeeId, setFormEmployeeId] = useState('');
    const [formQuantity, setFormQuantity] = useState('');
    const [formStatus, setFormStatus] = useState<'Active' | 'Inactive'>('Active');
    const [selectedTicket, setSelectedTicket] = useState<MealTicket | null>(null);
    const {
        modalOpen,
        setModalOpen,
        modalMode,
        saving,
        formError,
        validationError,
        setValidationError,
        openCreate,
        openEdit,
        submit
    } = useFormModal();

    async function load() {
        await loadData(async () => {
            const [t, e] = await Promise.all([ticketService.getAll(), employeeService.getAll()]);
            setTickets(t);
            setEmployees(e.filter(e => e.status === 'Active'));
        });
    }

    useEffect(() => {
        load();
    }, []);

    function handleOpenCreate() {
        setFormEmployeeId('');
        setFormQuantity('');
        setFormStatus('Active');
        setSelectedTicket(null);
        openCreate();
    }

    function handleOpenEdit(ticket: MealTicket) {
        setFormEmployeeId(ticket.employeeId);
        setFormQuantity(String(ticket.quantity));
        setFormStatus(ticket.status);
        setSelectedTicket(ticket);
        openEdit();
    }

    async function handleSave() {
        if (!formEmployeeId) {
            setValidationError('Selecione um funcionário.');
            return;
        }
        const qty = Number(formQuantity);
        if (!formQuantity || isNaN(qty) || qty <= 0) {
            setValidationError('Quantidade deve ser maior que 0.');
            return;
        }
        setValidationError(null);

        await submit(async () => {
            if (modalMode === 'create') {
                await ticketService.create({employeeId: formEmployeeId, quantity: qty});
            } else {
                await ticketService.update(selectedTicket!.id, {quantity: qty, status: formStatus});
            }
            await load();
        });
    }

    const active = tickets.filter(t => t.status === 'Active').length;
    const inactive = tickets.filter(t => t.status === 'Inactive').length;
    return (
        <div className="space-y-6">
            <div className="flex items-center justify-between">
                <h1 className="text-2xl font-bold">Tickets</h1>
                <Button onClick={handleOpenCreate} size="lg">
                    <Ticket className="h-4 w-4"/>
                    Novo Ticket
                </Button>
            </div>
            <div className="grid grid-cols-2 gap-4 sm:grid-cols-3">
                <StatCard label="Total" value={tickets.length}/>
                <StatCard label="Ativos" value={active} valueClassName="text-green-600"/>
                <StatCard label="Inativos" value={inactive} valueClassName="text-red-500"/>
            </div>

            {loading && <p className="text-muted-foreground text-sm">Carregando...</p>}
            {fetchError && <p className="text-red-500 text-sm">{fetchError}</p>}
            {!loading && !fetchError && (
                <>
                    <div className="hidden md:block rounded-md border">
                        <Table>
                            <TableHeader>
                                <TableRow>
                                    <TableHead>Funcionário</TableHead>
                                    <TableHead>Quantidade</TableHead>
                                    <TableHead>Situação</TableHead>
                                    <TableHead>Entregue em</TableHead>
                                    <TableHead/>
                                </TableRow>
                            </TableHeader>
                            <TableBody>
                                {tickets.map(t => (
                                    <TableRow key={t.id}>
                                        <TableCell>{t.employeeName}</TableCell>
                                        <TableCell>{t.quantity}</TableCell>
                                        <TableCell><StatusBadge status={t.status}/></TableCell>
                                        <TableCell>{new Date(t.deliveredAt).toLocaleDateString('pt-BR')}</TableCell>
                                        <TableCell className="text-right">
                                            <Button variant="ghost" size="icon" onClick={() => handleOpenEdit(t)}>
                                                <Pencil className="h-4 w-4"/>
                                            </Button>
                                        </TableCell>
                                    </TableRow>
                                ))}
                            </TableBody>
                        </Table>
                    </div>
                    <div className="md:hidden space-y-3">
                        {tickets.map(t => (
                            <div key={t.id} className="border rounded-lg p-4 flex items-start justify-between">
                                <div className="space-y-1">
                                    <p className="font-medium">{t.employeeName}</p>
                                    <p className="text-sm text-muted-foreground">Qtd: {t.quantity}</p>
                                    <p className="text-sm text-muted-foreground">{new Date(t.deliveredAt).toLocaleDateString('pt-BR')}</p>
                                    <StatusBadge status={t.status}/>
                                </div>
                                <Button variant="ghost" size="icon" onClick={() => handleOpenEdit(t)}>
                                    <Pencil className="h-4 w-4"/>
                                </Button>
                            </div>
                        ))}
                    </div>
                </>
            )}
            <Dialog open={modalOpen} onOpenChange={setModalOpen}>
                <DialogContent>
                    <DialogHeader>
                        <DialogTitle>{modalMode === 'create' ? 'Novo Ticket' : 'Editar Ticket'}</DialogTitle>
                    </DialogHeader>
                    <div className="space-y-4 pt-2">
                        <FormField label="Funcionário">
                            <Select value={formEmployeeId} onValueChange={(v) => setFormEmployeeId(v ?? '')}
                                    disabled={modalMode === 'edit'}>
                                <SelectTrigger className="w-full">
                                    <SelectValue>
                                        {employees.find(e => e.id === formEmployeeId)?.name ?? 'Selecione...'}
                                    </SelectValue>
                                </SelectTrigger>
                                <SelectContent>
                                    {employees.length === 0
                                        ? <p className="px-3 py-2 text-sm text-muted-foreground">Nenhum funcionário
                                            ativo.</p>
                                        : employees.map(e => (
                                            <SelectItem key={e.id} value={e.id}>{e.name}</SelectItem>
                                        ))
                                    }
                                </SelectContent>
                            </Select>
                        </FormField>
                        {modalMode === 'edit' && (
                            <FormField label="Situação">
                                <Select value={formStatus}
                                        onValueChange={(v) => setFormStatus((v ?? 'Active') as 'Active' | 'Inactive')}>
                                    <SelectTrigger className="w-full">
                                        <SelectValue>{formStatus === 'Active' ? 'Ativo' : 'Inativo'}</SelectValue>
                                    </SelectTrigger>
                                    <SelectContent>
                                        <SelectItem value="Active">Ativo</SelectItem>
                                        <SelectItem value="Inactive">Inativo</SelectItem>
                                    </SelectContent>
                                </Select>
                            </FormField>
                        )}
                        <FormField label="Quantidade">
                            <Input
                                type="number"
                                min={1}
                                value={formQuantity}
                                onChange={e => setFormQuantity(e.target.value)}
                                placeholder="Ex: 22"
                            />
                        </FormField>
                        {(validationError || formError) && (
                            <p className="text-sm text-red-500">{validationError ?? formError}</p>
                        )}
                        <div className="flex justify-end gap-2 pt-2">
                            <Button variant="outline" onClick={() => setModalOpen(false)}>Cancelar</Button>
                            <Button onClick={handleSave} disabled={saving}>
                                {saving ? 'Salvando...' : 'Salvar'}
                            </Button>
                        </div>
                    </div>
                </DialogContent>
            </Dialog>
        </div>
    );
}
