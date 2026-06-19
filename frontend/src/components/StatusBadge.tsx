import { Badge } from '@/components/ui/badge';
interface StatusBadgeProps {
    status: 'Active' | 'Inactive';
}
export default function StatusBadge({ status }: StatusBadgeProps) {
    return (
        <Badge variant={status === 'Active' ? 'default' : 'secondary'}>
            {status === 'Active' ? 'Ativo' : 'Inativo'}
        </Badge>
    );
}
