import React from "react";
import { Input } from "react-daisyui";
import MyIcon, { TypeFi } from "./MyIcon";

interface Props {
  label: string;
  type: string;
  name: string;
  value: string;
  onChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
  icon?: string;
  placeholder: string;
}

const MyInput: React.FC<Props> = ({
  label,
  type,
  name,
  value,
  onChange,
  icon,
  placeholder,
}) => {
  return (
    <div className="flex flex-col gap-2">
      <label htmlFor={name} className="text-md text-gray-500">
        {label}
      </label>
      <div className="relative">
        <Input
          type={type}
          id={name}
          name={name}
          value={value}
          onChange={onChange}
          placeholder={placeholder}
          className="pl-10 pr-4 py-3 rounded-lg border border-gray-300 hover:bg-gray-100 w-80 text-sm"
        />
        <div className="absolute inset-y-0 left-0 pl-3 flex items-center text-gray-400">
          <MyIcon icon={icon as TypeFi} />
        </div>
      </div>
    </div>
  );
};

export default MyInput;
