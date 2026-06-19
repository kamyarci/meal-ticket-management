export function formatCpf(cpf: string): string {
    return cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4');
}
export function buildDateTime(date: string, time: string, endOfDay: boolean): string {
    const offsetMinutes = new Date().getTimezoneOffset();
    const absOffset = Math.abs(offsetMinutes);
    const sign = offsetMinutes <= 0 ? '+' : '-';
    const hh = String(Math.floor(absOffset / 60)).padStart(2, '0');
    const mm = String(absOffset % 60).padStart(2, '0');
    const timeStr = time ? `${time}:00` : (endOfDay ? '23:59:59' : '00:00:00');
    return `${date}T${timeStr}${sign}${hh}:${mm}`;
}
