import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
interface StatCardProps {
    label: string;
    value: number;
    valueClassName?: string;
}
export default function StatCard({ label, value, valueClassName }: StatCardProps) {
    return (
        <Card>
            <CardHeader className="pb-2">
                <CardTitle className="text-sm font-medium text-muted-foreground">{label}</CardTitle>
            </CardHeader>
            <CardContent>
                <p className={`text-3xl font-bold ${valueClassName ?? ''}`}>{value}</p>
            </CardContent>
        </Card>
    );
}
