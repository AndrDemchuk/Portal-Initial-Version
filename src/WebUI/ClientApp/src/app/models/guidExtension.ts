export type GuidFormat = 'N' | undefined;

export class Guid {
  static newGuid(format?: GuidFormat) {
    const baseStr = Guid.getTemplate(format);
    return baseStr.replace(/[xy]/g, function (c) {
      const r = Math.random() * 16 | 0, v = c === 'x' ? r : (r & 0x3 | 0x8);
      return v.toString(16);
    });
  }

  private static getTemplate(format: GuidFormat) {
    if (format === 'N') return 'xxxxxxxxxxxx4xxxyxxxxxxxxxxxxxxx';
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx';
  }
}
