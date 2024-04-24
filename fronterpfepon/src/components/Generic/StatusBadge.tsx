interface Props {
    status: 1 | 0 | -1;
    text: string;
    className?: string;
}

const StatusBadge = ({ status, text, className = "" }: Props) => {
    return (
        <div
            className={`rounded-full w-40 px-my-8 border-2 text-center
        ${className}
                ${status === 1
                    ? "border-green-600 bg-green-100 "
                    : status === 0
                        ? "border-red-400 bg-red-100 "
                        : "border-neutral-500 bg-red-100"
                }`}
        >
            <p
                className={`font-semibold text-xsmall tracking-wide uppercase
                    p-2
                  ${status === 1
                        ? "text-green-600"
                        : status === 0
                            ? "text-red-400"
                            : "text-neutral-500"
                    }`}
            >
                {text}
            </p>
        </div>
    );
};

export default StatusBadge;