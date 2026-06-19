'use client';

import {useEffect, useState} from 'react';
import {IMaskInput} from 'react-imask';
import {UserPlus, Pencil} from 'lucide-react';
import {Button} from '@/components/ui/button';
import {Input} from '@/components/ui/input';
import {Dialog, DialogContent, DialogHeader, DialogTitle} from '@/components/ui/dialog';
import {Select, SelectContent, SelectItem, SelectTrigger, SelectValue} from '@/components/ui/select';
import {Table, TableBody, TableCell, TableHead, TableHeader, TableRow} from '@/components/ui/table';
import StatCard from '@/components/StatCard';
import StatusBadge from '@/components/StatusBadge';
import FormField from '@/components/FormField';
import {employeeService} from '@/services/employeeService';
import {Employee} from '@/types/employee';
import {useFormModal} from '@/hooks/useFormModal';
import {useAsync} from '@/hooks/useAsync';
import {formatCpf} from '@/utils/format';

export default function EmployeesPage() {
    const [employees, setEmployees] = useState<Employee[]>([]);
    const {loading, error: fetchError, run: loadData} = useAsync();

    const [formName, setFormName] = useState('');
    const [formCpf, setFormCpf] = useState('');
    const [formStatus, setFormStatus] = useState<'Active' | 'Inactive'>('Active');
    const [selectedEmployee, setSelectedEmployee] = useState<Employee | null>(null);

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
            const data = await employeeService.getAll();
            setEmployees(data);
        });
    }

    useEffect(() => {
        load();
    }, []);

    function handleOpenCreate() {
        setFormName('');
        setFormCpf('');
        setFormStatus('Active');
        setSelectedEmployee(null);
        openCreate();
    }

    function handleOpenEdit(employee: Employee) {
        setFormName(employee.name);
        setFormCpf(employee.cpf);
        setFormStatus(employee.status);
        setSelectedEmployee(employee);
        openEdit();
    }

    async function handleSave() {
        if (!formName.trim()) {
            setValidationError('Nome é obrigatório.');
            return;
        }
        if (formCpf.replace(/\D/g, '').length !== 11) {
            setValidationError('CPF inválido.');
            return;
        }
        setValidationError(null);

        await submit(async () => {
            const cleanCpf = formCpf.replace(/\D/g, '');
            if (modalMode === 'create') {
                await employeeService.create({name: formName.trim(), cpf: cleanCpf});
            } else {
                await employeeService.update(selectedEmployee!.id, {name: formName.trim(), status: formStatus});
            }
            await load();
        });
    }

    const active = employees.filter(e => e.status === 'Active').length;
    const inactive = employees.filter(e => e.status === 'Inactive').length;
    return (
        <div className="space-y-6">
            <div className="flex items-center justify-between">
                <h1 className="text-2xl font-bold">Funcionários</h1>
                <Button onClick={handleOpenCreate} size="lg">
                    <UserPlus className="h-4 w-4"/>
                    Novo Funcionário
                </Button>
            </div>

            <div className="grid grid-cols-2 gap-4 sm:grid-cols-3">
                <StatCard label="Total" value={employees.length}/>
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
                                    <TableHead>Nome</TableHead>
                                    <TableHead>CPF</TableHead>
                                    <TableHead>Situação</TableHead>
                                    <TableHead/>
                                </TableRow>
                            </TableHeader>
                            <TableBody>
                                {employees.map(emp => (
                                    <TableRow key={emp.id}>
                                        <TableCell>{emp.name}</TableCell>
                                        <TableCell>{formatCpf(emp.cpf)}</TableCell>
                                        <TableCell><StatusBadge status={emp.status}/></TableCell>
                                        <TableCell className="text-right">
                                            <Button variant="ghost" size="icon" onClick={() => handleOpenEdit(emp)}>
                                                <Pencil className="h-4 w-4"/>
                                            </Button>
                                        </TableCell>
                                    </TableRow>
                                ))}
                            </TableBody>
                        </Table>
                    </div>
                    <div className="md:hidden space-y-3">
                        {employees.map(emp => (
                            <div key={emp.id} className="border rounded-lg p-4 flex items-start justify-between">
                                <div className="space-y-1">
                                    <p className="font-medium">{emp.name}</p>
                                    <p className="text-sm text-muted-foreground">{formatCpf(emp.cpf)}</p>
                                    <StatusBadge status={emp.status}/>
                                </div>
                                <Button variant="ghost" size="icon" onClick={() => handleOpenEdit(emp)}>
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
                        <DialogTitle>{modalMode === 'create' ? 'Novo Funcionário' : 'Editar Funcionário'}</DialogTitle>
                    </DialogHeader>
                    <div className="space-y-4 pt-2">
                        <FormField label="Nome">
                            <Input value={formName} onChange={e => setFormName(e.target.value)}
                                   placeholder="Nome completo"/>
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
                        <FormField label="CPF">
                            <IMaskInput
                                mask="000.000.000-00"
                                value={formCpf}
                                onAccept={(value: string) => setFormCpf(value)}
                                disabled={modalMode === 'edit'}
                                placeholder="000.000.000-00"
                                className="flex h-9 w-full rounded-md border border-input bg-transparent px-3 py-1 text-sm shadow-sm transition-colors placeholder:text-muted-foreground focus-visible:outline-none focus-visible:ring-1 focus-visible:ring-ring disabled:opacity-50"
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
