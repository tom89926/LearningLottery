export const deepCopy = <T>(value: T) => {
  return JSON.parse(JSON.stringify(value));
};
