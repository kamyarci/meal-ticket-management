import Link from 'next/link';

export default function NotFound() {
    return (
        <div className="flex flex-col items-center justify-center gap-4 py-20">
            <h1 className="text-4xl font-bold">404</h1>
            <p className="text-muted-foreground">Pagina nao encontrada.</p>
            <Link href="/employees" className="inline-flex items-center justify-center rounded-lg bg-primary px-4 py-2 text-sm font-medium text-primary-foreground transition-colors hover:bg-primary/80">
                Voltar ao inicio
            </Link>
        </div>
    );
}
