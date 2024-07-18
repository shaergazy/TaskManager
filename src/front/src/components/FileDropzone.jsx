import React, { useCallback } from 'react';
import { useDropzone } from 'react-dropzone';
import { Box, Text } from '@chakra-ui/react';

const FileDropzone = ({ onDrop }) => {
  const onDropAccepted = useCallback((acceptedFiles) => {
    onDrop(acceptedFiles);
  }, [onDrop]);

  const { getRootProps, getInputProps, isDragActive } = useDropzone({
    onDrop: onDropAccepted,
    accept: 'application/pdf, application/msword, application/vnd.openxmlformats-officedocument.wordprocessingml.document',
  });

  return (
    <Box
      {...getRootProps()}
      p={24}
      borderWidth={2}
      borderRadius="lg"
      borderColor={isDragActive ? 'teal.400' : 'gray.200'}
      bg={isDragActive ? 'gray.100' : 'white'}
      textAlign="center"
      cursor="pointer"
      _hover={{ bg: 'gray.50' }}
    >
      <input {...getInputProps()} />
      {
        isDragActive ? (
            <Text>Drag files here...</Text>
        ) : (
          <Text>Drag files here or click to select files</Text>
        )
      }
    </Box>
  );
};

export default FileDropzone;
