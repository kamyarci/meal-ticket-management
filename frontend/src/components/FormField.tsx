interface FormFieldProps {
    label: string;
    children: React.ReactNode;
}
export default function FormField({ label, children }: FormFieldProps) {
    return (
        <div className="space-y-1">
            <label className="text-sm font-medium">{label}</label>
            {children}
        </div>
    );
}
