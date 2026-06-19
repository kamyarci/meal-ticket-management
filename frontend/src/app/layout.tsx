import type { Metadata } from 'next';
import './globals.css';
import Header from '@/components/Header';
import { Geist } from "next/font/google";
import { cn } from "@/lib/utils";

const geist = Geist({subsets:['latin'],variable:'--font-sans'});
export const metadata: Metadata = {
    title: 'Meal Ticket Management',
    description: 'Gerenciamento de tickets de refeição',
};
export default function RootLayout({ children }: { children: React.ReactNode }) {
    return (
        <html lang="pt-BR" className={cn("font-sans", geist.variable)}>
            <body className="min-h-screen bg-background antialiased">
                <Header />
                <main className="max-w-5xl mx-auto px-4 py-6">
                    {children}
                </main>
            </body>
        </html>
    );
}
