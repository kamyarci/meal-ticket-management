import type { Metadata } from 'next';
import './globals.css';

export const metadata: Metadata = {
    title: 'Meal Ticket Management',
    description: 'Gerenciamento de tickets de refeição',
};

export default function RootLayout({ children }: { children: React.ReactNode }) {
    return (
        <html lang="pt-BR">
            <body className="min-h-screen bg-background antialiased">
                {children}
            </body>
        </html>
    );
}
