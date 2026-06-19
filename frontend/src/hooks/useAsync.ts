'use client';

import {useState} from 'react';

export function useAsync() {
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    async function run(action: () => Promise<void>) {
        setError(null);
        setLoading(true);
        try {
            await action();
        } catch (err) {
            setError((err as Error).message);
        } finally {
            setLoading(false);
        }
    }

    return {loading, error, setError, run};
}
