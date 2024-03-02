import { Notify } from 'quasar';

let currentNotify: (() => void) | null = null;

interface NotifyOptions {
  color?: string;
  textColor?: string;
  icon?: string;
  message?: string;
}

export const successNotify = ({
  color,
  textColor,
  icon,
  message,
}: NotifyOptions = {}): void => {
  if (currentNotify !== null) {
    currentNotify();
  }

  currentNotify = Notify.create({
    color: color ?? 'green-4',
    textColor: textColor ?? 'white',
    icon: icon ?? 'cloud_done',
    message: message ?? 'Success',
  });
};

export const errorNotify = ({
  color,
  textColor,
  icon,
  message,
}: NotifyOptions = {}): void => {
  if (currentNotify !== null) {
    currentNotify();
  }

  currentNotify = Notify.create({
    color: color ?? 'red-5',
    textColor: textColor ?? 'white',
    icon: icon ?? 'error',
    message: message ?? 'Error',
  });
};

export const showNotify = ({
  color,
  textColor,
  icon,
  message,
}: NotifyOptions): void => {
  if (currentNotify !== null) {
    currentNotify();
  }

  currentNotify = Notify.create({
    color: color,
    textColor: textColor,
    icon: icon,
    html: true,
    message: message,
  });
};

export default showNotify;
