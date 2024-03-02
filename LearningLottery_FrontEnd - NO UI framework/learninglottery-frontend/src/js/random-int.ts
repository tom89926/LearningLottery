const randomInt = ({
  min,
  max,
  count,
}: {
  min: number;
  max: number;
  count: number;
}): number[] => {
  const tempArr: number[] = [];
  while (tempArr.length < count) {
    const num = Math.floor(Math.random() * (max - min + 1) + min);
    if (tempArr.includes(num)) {
      continue;
    }
    tempArr.push(num);
  }
  return tempArr;
};

export default randomInt;
