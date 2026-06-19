'use client';

import { useState } from 'react';

export type ModalMode = 'create' | 'edit';
export function useFormModal() {
    const [modalOpen, setModalOpen] = useState(false);
    const [modalMode, setModalMode] = useState<ModalMode>('create');
    const [saving, setSaving] = useState(false);
    const [formError, setFormError] = useState<string | null>(null);
    const [validationError, setValidationError] = useState<string | null>(null);

    function openCreate() {
        setModalMode('create');
        setFormError(null);
        setValidationError(null);
        setModalOpen(true);
    }
    function openEdit() {
        setModalMode('edit');
        setFormError(null);
        setValidationError(null);
        setModalOpen(true);
    }
    function closeModal() {
        setModalOpen(false);
    }
    async function submit(action: () => Promise<void>) {
        setSaving(true);
        setFormError(null);
        try {
            await action();
            closeModal();
        } catch (err) {
            setFormError((err as Error).message);
        } finally {
            setSaving(false);
        }
    }

    return {
        modalOpen, setModalOpen,
        modalMode,
        saving,
        formError,
        validationError, setValidationError,
        openCreate, openEdit, closeModal, submit,
    };
}
