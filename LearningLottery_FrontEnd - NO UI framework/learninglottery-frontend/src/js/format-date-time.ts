import moment from 'moment';

export const FormatDate = ({ dateStr }: { dateStr: string }): string => {
  return moment(dateStr).format('YYYY-MM-DD');
};

export const FormatDateTime = ({
  dateTimeStr,
}: {
  dateTimeStr: string;
}): string => {
  return moment(dateTimeStr).format('YYYY-MM-DD HH:mm:ss');
};
