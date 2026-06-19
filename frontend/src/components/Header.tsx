'use client';

import Link from 'next/link';
import Image from 'next/image';
import {usePathname} from 'next/navigation';
import {useState} from 'react';
import {Menu, X} from 'lucide-react';

const navLinks = [
    {href: '/employees', label: 'Funcionários'},
    {href: '/tickets', label: 'Tickets'},
    {href: '/report', label: 'Relatório'},
];
export default function Header() {
    const pathname = usePathname();
    const [menuOpen, setMenuOpen] = useState(false);

    return (
        <header className="border-b bg-background">
            <div className="max-w-5xl mx-auto px-4 flex items-center justify-between h-14">
                <div className="flex items-center gap-2 font-semibold text-foreground">
                    <Image
                        src="/icon.png"
                        alt="MealTicket"
                        width={32}
                        height={32}
                        className="rounded-full object-cover"
                    />
                    <span>MealTicket</span>
                </div>
                <nav className="hidden md:flex items-center gap-6">
                    {navLinks.map(link => (
                        <Link
                            key={link.href}
                            href={link.href}
                            className={`text-sm transition-colors hover:text-foreground ${
                                pathname === link.href
                                    ? 'text-foreground font-medium border-b-2 border-foreground pb-0.5'
                                    : 'text-muted-foreground'
                            }`}
                        >
                            {link.label}
                        </Link>
                    ))}
                </nav>
                <button
                    className="md:hidden text-muted-foreground"
                    onClick={() => setMenuOpen(prev => !prev)}
                >
                    {menuOpen ? <X className="h-5 w-5"/> : <Menu className="h-5 w-5"/>}
                </button>
            </div>
            {menuOpen && (
                <div className="md:hidden border-t px-4 py-3 flex flex-col gap-3 bg-background">
                    {navLinks.map(link => (
                        <Link
                            key={link.href}
                            href={link.href}
                            onClick={() => setMenuOpen(false)}
                            className={`text-sm ${
                                pathname === link.href
                                    ? 'text-foreground font-medium'
                                    : 'text-muted-foreground'
                            }`}
                        >
                            {link.label}
                        </Link>
                    ))}
                </div>
            )}
        </header>
    );
}
